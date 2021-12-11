# breaking-news-api

API that lists contents from "https://haberler.com/sondakika" in JSON format

## API Reference

#### Get all news

```http
GET /api/news
```

#### Get single news

```http
GET /api/news/{id}
```

| Parameter | Type     | Required                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `integer` | `true` |


