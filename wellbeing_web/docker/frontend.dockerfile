FROM node:16.17.0

#Installing WEBPACK
RUN npm install -g @sanity/cli netlify-cli nuxt

#Installing https driver
RUN apt-get update && apt-get install apt-transport-https unzip wget -y