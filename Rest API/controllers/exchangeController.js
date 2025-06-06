const model = require('../models/exchangeModel');

exports.getOffers = async (req, res) => {
  const offers = await model.getAllOffers();
  res.json(offers);
};

exports.createOffer = async (req, res) => {
  const { user_id, offered_plant_id, desired_plant_id } = req.body;
  if (!user_id || !offered_plant_id || !desired_plant_id) {
    return res.status(400).json({ error: 'Все поля обязательны' });
  }

  const offer = await model.createOffer(user_id, offered_plant_id, desired_plant_id);
  res.status(201).json(offer);
};

exports.deleteOffer = async (req, res) => {
  const id = req.params.id;
  await model.deleteOffer(id);
  res.status(204).send();
};

exports.acceptOffer = async (req, res) => {
  const exchangeId = req.params.id;
  const { accepted_by } = req.body;

  if (!accepted_by) {
    return res.status(400).json({ error: 'Не выбран пользователь, принимающий обмен' });
  }

  try {
    const offer = await model.getExchangeById(exchangeId);
    
    // Проверка: если принимающий пользователь и создавший обмен — это один и тот же пользователь
    if (offer.user_id === parseInt(accepted_by)) {
      return res.status(400).json({ error: 'Вы не можете принять обмен с таким же именем' });
    }

    await model.acceptOffer(exchangeId, accepted_by);
    res.status(200).json({ message: 'Обмен закрыт' });

  } catch (err) {
    console.error(err);
    res.status(500).json({ error: 'Ошибка при завершении обмена' });
  }
};


exports.getHistory = async (req, res) => {
  const history = await model.getExchangeHistory();
  res.json(history);
};



