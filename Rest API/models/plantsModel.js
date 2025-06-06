const db = require('../config/db');

const getAllPlants = async () => {
  const result = await db.query('SELECT * FROM plants order by id');
  return result.rows;
};

const createPlant = async (name) => {
  const result = await db.query(
    'INSERT INTO plants (name) VALUES ($1) RETURNING *',
    [name]
  );
  return result.rows[0];
};

const deletePlant = async (id) => {
  await db.query('DELETE FROM plants WHERE id = $1', [id]);
};

const updatePlant = async (id, name) => {
  const result = await db.query(
    'UPDATE plants SET name = $1 WHERE id = $2 RETURNING *',
    [name, id]
  );
  return result.rows[0];
};

module.exports = {
  getAllPlants,
  createPlant,
  deletePlant,
  updatePlant
};
