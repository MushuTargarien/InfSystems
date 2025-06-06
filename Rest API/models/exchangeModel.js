const db = require('../config/db');

const getAllOffers = async () => {
  const result = await db.query(`
    SELECT eo.id, eo.user_id, u.name AS user_name,
           eo.offered_plant_id, p1.name AS offered_plant,
           eo.desired_plant_id, p2.name AS desired_plant
    FROM exchange_offers eo
    JOIN users u ON eo.user_id = u.id
    JOIN plants p1 ON eo.offered_plant_id = p1.id
    JOIN plants p2 ON eo.desired_plant_id = p2.id
  `);
  return result.rows;
};

const createOffer = async (user_id, offered_plant_id, desired_plant_id) => {
  const result = await db.query(`
    INSERT INTO exchange_offers (user_id, offered_plant_id, desired_plant_id)
    VALUES ($1, $2, $3) RETURNING *
  `, [user_id, offered_plant_id, desired_plant_id]);
  return result.rows[0];
};

const deleteOffer = async (id) => {
  await db.query('DELETE FROM exchange_offers WHERE id = $1', [id]);
};

const acceptOffer = async (exchangeId, acceptedBy) => {
  const res = await db.query('SELECT * FROM exchange_offers WHERE id = $1', [exchangeId]);
  const offer = res.rows[0];
  if (!offer) throw new Error('Обмен не найден');

  await db.query(`
    INSERT INTO exchange_history ( user_id, offered_plant_id, desired_plant_id, accepted_by)
    VALUES ($1, $2, $3, $4)
  `, [
    offer.user_id,
    offer.offered_plant_id,
    offer.desired_plant_id,
    acceptedBy
  ]);

  await db.query('DELETE FROM exchange_offers WHERE id = $1', [exchangeId]);
};

const getExchangeHistory = async () => {
  const result = await db.query(`
    SELECT h.*, 
           u1.name AS creator_name,
           u2.name AS accepter_name,
           p1.name AS offered_name,
           p2.name AS desired_name
    FROM exchange_history h
    JOIN users u1 ON h.user_id = u1.id
    JOIN users u2 ON h.accepted_by = u2.id
    JOIN plants p1 ON h.offered_plant_id = p1.id
    JOIN plants p2 ON h.desired_plant_id = p2.id
    ORDER BY h.timestamp DESC
  `);
  return result.rows;
};

const getExchangeById = async (exchangeId) => {
  const result = await db.query('SELECT * FROM exchange_offers WHERE id = $1', [exchangeId]);
  return result.rows[0]; // возвращаем первый  результат
};

module.exports = {
  getAllOffers,
  createOffer,
  deleteOffer,
  acceptOffer,
  getExchangeById,
  getExchangeHistory,
};
