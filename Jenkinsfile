pipeline {
    agent any

    tools {
        dotnetsdk 'dotnet-sdk-8.0'
    }

    stages {
        stage('Clean workspace') {
            steps {
                script {
                    deleteDir() 
                }
            }
        }

        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Linases/ApiTestFramework.git'
            }
        }

        stage('Restore') {
            steps {
                bat "dotnet restore ApiTestFramework.sln"
            }
        }

        stage('Build') {
            steps {
                bat "dotnet build ApiTestFramework.sln --no-restore"
            }
        }

        stage('Test') {
            steps {
                bat "dotnet test ApiTestFramework.sln --no-build --logger \"trx;LogFileName=test_results.trx\""
            }
        }
    }

    post {
        always {
            junit '**/TestResults/*.xml'
        }
    }
}
