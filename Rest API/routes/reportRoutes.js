const express = require('express');
const router = express.Router();
const controller = require('../controllers/reportController');

router.get('/download', controller.downloadReport);

module.exports = router;

