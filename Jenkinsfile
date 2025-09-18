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
                bat "mkdir TestResults"
                bat "dotnet test Diploma_OrangeHRM.sln --no-build --logger \\\"junit;LogFilePath=TestResults\\\\test-results.xml\\\""
            }
        }
    }

    post {
        always {
            junit '**/TestResults/*.xml'
        }
    }
}
