pipeline {
    agent any

    tools {
        dotnetsdk 'dotnet-sdk-6.0'
    }

    environment {
        SOLUTION_DIR = 'ApiTestFramework'
    }

    stages {
        stage('Clean workspace') {
            steps {
                bat 'rmdir /s /q ApiTestFramework.sln || exit 0'
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
