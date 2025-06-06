const { Document, Packer, Paragraph, TextRun } = require('docx');
const db = require('../config/db');

exports.downloadReport = async (req, res) => {
  try {
    const users = await db.query(`
      SELECT u.name, COUNT(*) AS count
      FROM exchange_offers eo
      JOIN users u ON eo.user_id = u.id
      GROUP BY u.name
      ORDER BY count DESC
      LIMIT 5
    `);

    const plants = await db.query(`
      SELECT p.name, COUNT(*) AS count
      FROM exchange_offers eo
      JOIN plants p ON eo.offered_plant_id = p.id
      GROUP BY p.name
      ORDER BY count DESC
      LIMIT 5
    `);

   const doc = new Document({
      sections: [{
        children: [
          new Paragraph({
            children: [new TextRun({ text: 'Отчёт об активности', bold: true, size: 28 })],
            spacing: { after: 300 }
          }),
          new Paragraph({ text: 'Самые активные пользователи:', spacing: { after: 200 }  }),
          ...users.rows.map(u =>
            new Paragraph(`${u.name} — ${u.count} обменов`)
          ),
          new Paragraph({ text: '', spacing: { after: 300 } }),
          new Paragraph({ text: 'Самые популярные растения:', spacing: { after: 200 }}),
          ...plants.rows.map(p =>
            new Paragraph(`${p.name} — ${p.count} предложений`)
          )
        ]
      }]
    });

    const buffer = await Packer.toBuffer(doc);

    res.setHeader('Content-Disposition', 'attachment; filename=отчёт.docx');                                        // авто-скачивание и заголовок
    res.setHeader('Content-Type', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document');       // Указывает для браузера что это врод документ
    res.send(buffer);                                                                                               // отправка клиенту
  } catch (err) {
    console.error(err);
    res.status(500).json({ error: 'Ошибка генерации отчёта' });
  }
};
