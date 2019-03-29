FROM node:10 as react-build

ARG api_url=observable-controller.herokuapp.com
ENV REACT_APP_API_URL=${api_url}

WORKDIR /app
COPY . ./
RUN npm install
RUN npm run build

FROM nginx:latest
COPY --from=react-build /app/build /usr/share/nginx/html

# docker build -t observableapp .
# docker run -d -p 3000:80 --name observableapp-local observableapp
