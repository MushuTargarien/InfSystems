const model = require('../models/plantsModel');

exports.getPlants = async (req, res) => {
  const plants = await model.getAllPlants();
  res.json(plants);
};

exports.createPlant = async (req, res) => {
  const { name } = req.body;
  if (!name) {
    return res.status(400).json({ error: 'Название обязательно' });
  }

  try {
    const plant = await model.createPlant(name);
    res.status(201).json(plant);
  } catch (err) {
    if (err.code === '23505') {
      // PostgreSQL код ошибки для "уникальное значение нарушено"
      return res.status(409).json({ error: 'Растение с таким названием уже существует' });
    }
    console.error(err);
    res.status(500).json({ error: 'Ошибка сервера' });
  }
};


exports.deletePlant = async (req, res) => {
  const id = req.params.id;
  await model.deletePlant(id);
  res.status(204).send();
};

exports.updatePlant = async (req, res) => {
  const id = req.params.id;
  const { name } = req.body;
  if (!name) return res.status(400).json({ error: 'Name is required' });

  const updated = await model.updatePlant(id, name);
  res.status(200).json(updated);
};
