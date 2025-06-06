const express = require('express');
const router = express.Router();
const controller = require('../controllers/exchangeController');

router.get('/', controller.getOffers);
router.post('/', controller.createOffer);
router.delete('/:id', controller.deleteOffer);
router.get('/history', controller.getHistory);
router.put('/:id/accept', controller.acceptOffer);


module.exports = router;
