# Используем официальный Node.js образ
FROM node:20-alpine

# Рабочая директория
WORKDIR /usr/src/app

# Копируем package.json и package-lock.json
COPY package*.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем исходный код
COPY . .

# Генерируем Prisma клиент
RUN npx prisma generate

# Собираем проект
RUN npm run build

# Открываем порт
EXPOSE 3001

# Запускаем приложение
CMD ["node", "dist/main.js"]