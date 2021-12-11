const rssParser = require('rss-parser');
const { v4: uuidv4 } = require('uuid');

const parser = new rssParser({
    customFields: {
        item: ['media:content']
    }
});

const news = [];

const getNews = async () => {
    const rss = await parser.parseURL('https://rss.haberler.com/rss.asp?kategori=sondakika');
    rss.items.map((item, i) => {
        const d = new Date(item.isoDate);
        const hour = (d.getHours() < 10 ? "0" + d.getHours() : d.getHours()) + ":" + (d.getMinutes() < 10 ? "0" + d.getMinutes() : d.getMinutes());
        news.push({
            id: i + 100000,
            title: item.title,
            summary: item.content,
            image: item['media:content']['$'].url,
            time: hour,
            url: item.link,
            uniqId: uuidv4(),
            source: {
                title: 'Haberler.com',
                url: 'https://haberler.com',
                logo: 'https://www.haberler.com/static/img/tasarim/haberler-logo.svg',
            }
        });
    });
    return news;
}

module.exports = getNews();
