const express = require('express');
const router = express.Router();
const controller = require('../controllers/plantsController');

router.get('/', controller.getPlants);
router.post('/', controller.createPlant);
router.delete('/:id', controller.deletePlant);
router.put('/:id', controller.updatePlant);

module.exports = router;
