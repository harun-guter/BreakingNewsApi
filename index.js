const news = require('./news');
const express = require('express');
const getNews = require('./news');

const app = express();

app.get('/api/news', (request, response) => {
    getNews.then((data) => response.send({
        totalNews: data.length,
        news: data,
    }))
    console.log(getNews);
});

app.listen(3000);