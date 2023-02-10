echo "*** Starting DOCKER containers... ***"
docker-compose build

echo "*** Running Docker containers, and starting API at --port 5000.... ****"
docker-compose up -d

echo "*** Updating node dependencies. *** "
docker-compose exec wellbeing_web yarn install

echo "*** Starting Sanity. *** "
docker-compose exec -d wellbeing_web yarn --cwd ./wellbeingcms start

echo "*** Runnin Gulp. *** "
docker-compose exec wellbeing_web yarn run ntl dev
#run dev

