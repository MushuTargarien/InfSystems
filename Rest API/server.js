const express = require('express');
const cors = require('cors');
const app = express();
const plantRoutes = require('./routes/plantsRoutes');
const exchangeRoutes = require('./routes/exchangeRoutes');
const userRoutes = require('./routes/usersRoutes');
const reportRoutes = require('./routes/reportRoutes');

app.use(cors());
app.use(express.json());
app.use(express.static('public'));
app.use('/api/plants', plantRoutes);
app.use('/api/exchange', exchangeRoutes);
app.use('/api/users', userRoutes);
app.use('/api/reports', reportRoutes);

const PORT = 3000;
app.listen(PORT, () => console.log(`Server running on http://localhost:${PORT}`));
