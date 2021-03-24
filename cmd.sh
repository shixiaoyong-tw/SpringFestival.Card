#! /usr/bin/env bash


build() {
  docker build -t spring .
}




# "AWS_ACCESS_KEY_ID": "DUMMYIDEXAMPLE",
# "AWS_SECRET_ACCESS_KEY": "DUMMYEXAMPLEKEY",
# "REGION":"eu-west-1"



run() {
    echo "start to run the appliation..."
    export ASPNETCORE_ENVIRONMENT="Development"
    export AWS_ACCESS_KEY_ID="DUMMYIDEXAMPLE"
    export AWS_SECRET_ACCESS_KEY="DUMMYEXAMPLEKEY"
    export REGION="eu-west-1"

    docker run --rm --name test  -p 8080:80 spring 
}


up() {
    docker-compose up
}

case $1 in 
  build) "$@"; exit;;
  run) "$@"; exit;;
  up) "$@"; exit;;
esac

echo "wrang command..."
exit 1