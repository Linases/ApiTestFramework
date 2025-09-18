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
                bat 'rmdir /s /q %SOLUTION_DIR% || exit 0'
            }
        }

        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Linases/ApiTestFramework.git'
            }
        }

        stage('Restore') {
            steps {
                 bat "dotnet restore ApiTestFramework/ApiTestFramework.sln"
            }
        }

        stage('Build') {
            steps {
                bat "dotnet build %SOLUTION_DIR% --no-restore"
            }
        }

        stage('Test') {
            steps {
                bat "dotnet test %SOLUTION_DIR% --no-build --logger \"trx;LogFileName=test_results.trx\""
            }
        }
    }

    post {
        always {
             junit '**/TestResults/*.xml'
        }
    }
}
