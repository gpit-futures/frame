FROM node:carbon-alpine as build
RUN apk add --no-cache git
WORKDIR /app
COPY ./ui .

RUN npm i
RUN npm run build

FROM nginx:alpine
WORKDIR /var/www/html
COPY --from=build /app/dist .
COPY --from=build /app/nginx.conf /etc/nginx/nginx.conf
