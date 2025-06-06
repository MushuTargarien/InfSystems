const db = require('../config/db');

const getAllUsers = async () => {
  const result = await db.query('SELECT * FROM users ORDER BY id');
  return result.rows;
};

const createUser = async (name) => {
  const result = await db.query(
    'INSERT INTO users (name) VALUES ($1) RETURNING *',
    [name]
  );
  return result.rows[0];
};

const updateUser = async (id, name) => {
  const result = await db.query(
    'UPDATE users SET name = $1 WHERE id = $2 RETURNING *',
    [name, id]
  );
  return result.rows[0];
};

const deleteUser = async (id) => {
  await db.query('DELETE FROM users WHERE id = $1', [id]);
};

module.exports = {
  getAllUsers,
  createUser,
  updateUser,
  deleteUser
};
