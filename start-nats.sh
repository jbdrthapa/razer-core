# docker run -d --restart=always -p 4222:4222 nats
# docker run -d --restart=no -p 4222:4222 nats
docker run -d --restart=always --name nats -p 4222:4222 -p 8222:8222 nats --http_port 8222
