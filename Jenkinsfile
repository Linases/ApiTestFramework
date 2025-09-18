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
                bat "if exist TestResults rmdir /s /q TestResults"
                bat "mkdir TestResults"
                bat "dotnet test ApiTestFramework.sln --no-build --logger \"trx;LogFileName=TestResults\\test_results.trx\""
                bat "dotnet tool install -g trx2junit"
                bat "%USERPROFILE%\\.dotnet\\tools\\trx2junit TestResults\\test_results.trx"
                bat "dir TestResults"
            }
        }
    }

    post {
        always {
            junit "TestResults/test_results.xml"
        }
    }
}
