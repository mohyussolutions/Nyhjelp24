# Docker Management Commands

Here are the essential commands to manage the NaboHjelp24 Docker environment.

## Start Application

To build and start all services in the background:

docker-compose up --build -d

## Stop Application

To stop and remove containers (and networks):

docker-compose down

## View Logs

To view logs from all containers in real-time:

docker-compose logs -f

## Check Status

To check the status of running containers:

docker-compose ps

## Stop Specific Service

To stop just the frontend or backend:

docker stop nabohjelp-frontend

# or

docker stop nabohjelp-backend

## Clean Up

To remove all unused containers, networks, images (both dangling and unreferenced):

docker system prune -a
