pipeline {
    agent any
    stages {
        stage('git repo & clean') {
            steps {
                bat "rmdir /s /q ApiTestFramework  || exit 0"
                bat "git clone https://github.com/Linases/ApiTestFramework.git"
                bat "mvn clean -f ApiTestFramework"
            }
        }
        stage('install') {
            steps {
                bat "mvn install -f ApiTestFramework"
            }
        }
        stage('test') {
            steps {
                bat "mvn test -f TApiTestFramework"
            }
        }
        stage('package') {
            steps {
                bat "mvn package -f ApiTestFramework"
            }
        }
    }
}
