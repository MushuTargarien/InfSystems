const model = require('../models/usersModel');

exports.getUsers = async (req, res) => {
  const users = await model.getAllUsers();
  res.json(users);
};

exports.createUser = async (req, res) => {
  const { name } = req.body;
  if (!name) return res.status(400).json({ error: 'Имя обязательно' });

  const user = await model.createUser(name);
  res.status(201).json(user);
};

exports.updateUser = async (req, res) => {
  const { name } = req.body;
  const id = req.params.id;
  if (!name) return res.status(400).json({ error: 'Имя обязательно' });

  const updated = await model.updateUser(id, name);
  res.status(200).json(updated);
};

exports.deleteUser = async (req, res) => {
  const id = req.params.id;
  await model.deleteUser(id);
  res.status(204).send();
};
