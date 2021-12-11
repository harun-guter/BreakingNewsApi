const news = require('./news');
const express = require('express');
const getNews = require('./news');

const app = express();

app.get('/api/news', (request, response) => {
    response.contentType('application/json');
    getNews.then((data) => response.send({
        totalNews: data.length,
        news: data,
    }))
});

app.get('/api/news/*', (request, response) => {
    response.contentType("application/json");
    getNews.then((data) => {
        data.find(x => x.id === 1 * request.url.split('/')[3]) !== undefined ?
            response.send(data.find(x => x.id === (1 * request.url.split('/')[3]))) :
            response.redirect('/api/news');
    })
});

app.listen(3000);