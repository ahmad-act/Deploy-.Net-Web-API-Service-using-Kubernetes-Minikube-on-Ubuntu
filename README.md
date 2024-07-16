# Deploy .Net Web API Service using Kubernetes (Minikube) on Ubuntu

## Install Git

### Summary of Git Commands

```sh
sudo apt update
sudo apt install git
git –version
```

### Step 1: Update the Package Index

#### First, update your existing list of packages:

```sh
sudo apt update
```

### Step 2: Install Git

#### Install Git using the apt package manager:

```sh
sudo apt install git
```

### Step 3: Verify the Installation

#### Verify that Git has been installed correctly by checking its version:

```sh
git --version
```

#### You should see output similar to this (the version number may be different):

```sh
git version 2.25.1
```

 
### Git Configuration

#### Git configuration is essential for setting up your identity and preferences when working with Git repositories. Configuration settings are stored in configuration files, and you can set them at three different levels:

 1. **System Level:** Applies to all users on the system and all their repositories. (/etc/gitconfig)
 2. **Global Level:** Applies to all repositories for your user. (~/.gitconfig)
 3. **Local Level:** Applies to a specific repository. (.git/config in the repository)

#### Step 1: Setting Up Your Identity

##### When you make a commit in Git, it includes your name and email address. You should set these to identify your commits.

###### Set Your Name

```sh
git config --global user.name "Your Name"
```

###### Set Your Email

```sh
git config --global user.email "your.email@example.com"
```

#### Step 2: Checking Your Configuration

 1. View All Configurations
 
 ##### To see all Git configurations that are currently set, use the following command:
 
 ```sh
 git config --list
 ```

 ##### This will list all the configuration settings that are in effect, including those from the  system, global, and local levels.
 
 2. View Specific Configuration
 
 ##### To check a specific configuration setting, use the following command format:
 
 ```sh
 git config <key>
 ```
 
 ##### For example, to check your user name and email:
 
 ```sh
 git config user.name
 git config user.email
 ```

 3. View Configurations by Level
 
 ##### Git allows you to specify the level at which you want to view configurations:
 
 ###### System Level
 
 **To view system-level configurations (applies to all users and repositories):**
 
 ```sh
 git config --system --list
 ```

 ###### Global Level

 **To view global-level configurations (applies to all repositories for the current user):**
 
 ```sh
 git config --global --list
 ```

 ###### Local Level

 **To view local-level configurations (applies only to the current repository):**
 
 ```sh
 git config --local --list
 ```

 4. Inspect Configuration Files
 
 ##### You can directly view and edit the Git configuration files:
 
 ##### Global Configuration File

 ###### To view and edit the global configuration file:
 
 ```sh
 nano ~/.gitconfig
 ```

 ##### System Configuration File

 ###### To view and edit the system configuration file:

```sh
 sudo nano /etc/gitconfig
 ```

 ##### Local Configuration File
 
 ###### To view and edit the local configuration file (within a Git repository):
 
 ```sh
 nano .git/config
```

#### Summary of Git Configuration Verification Commands

##### Here is a summary of useful commands to check Git configurations:

```sh
# View all configurations
git config --list

# View system-level configurations
git config --system --list

# View global-level configurations
git config --global --list

# View local-level configurations
git config --local --list

# Check specific configuration settings
git config user.name
git config user.email

# View and edit global configuration file
nano ~/.gitconfig

# View and edit system configuration file
sudo nano /etc/gitconfig

# View and edit local configuration file (within a repository)
nano .git/config
```


## Install Docker

### Step 1: Update and Upgrade Packages

#### First, update your existing list of packages:

```sh
sudo apt update
```
#### Then, upgrade the installed packages to the latest versions:

```sh
sudo apt upgrade
```

### Step 2: Install apt-utils

#### Install apt-utils to ensure smooth package installations:

```sh
sudo apt-get install -y apt-utils
```

### Step 3: Install Docker

#### Install Docker from the default Ubuntu repositories:

```sh
sudo apt install -y docker.io
```

### Step 4: Start and Enable Docker

#### Start the Docker service:

```sh
sudo systemctl start docker
```

#### Enable Docker to start on boot:

```sh
sudo systemctl enable docker
```

### Step 5: Add Your User to the Docker Group

#### To avoid needing sudo for every Docker command, add your user to the docker group:

```sh
sudo usermod -aG docker $USER
```

##### Replace $USER with your actual username (i.e. ubuntu) if necessary.

### Step 6: Log Out and Log Back In

#### Log out and log back in to apply the changes to your user group.

### Step 7: Verify Docker Installation

#### Check the Docker version to ensure it is installed correctly:

```sh
docker --version
```

### Step 8: Check Docker Service Status

#### Verify that the Docker service is running:

```sh
sudo systemctl status docker
```

### Step 9: Enable Docker Service to Start on Boot (if needed)

#### If not already done, ensure Docker starts on boot:

```sh
sudo systemctl enable docker
```

### Step 10: Run a Test Container

#### Finally, run a test container to verify that Docker is working correctly:

```sh
docker run hello-world
```

##### This command pulls the hello-world image from Docker Hub and runs it in a container. You should see a message that confirms Docker is installed and running correctly.

## Install kubectl

### Summary of kubectl Commands

#### Here is a summary of all the commands for easy reference:

```sh
# Download the latest release of kubectl
curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"

# (Optional) Download the checksum file
curl -LO "https://dl.k8s.io/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl.sha256"

# (Optional) Verify the checksum
echo "$(cat kubectl.sha256) kubectl" | sha256sum --check

# Make the kubectl binary executable
chmod +x kubectl

# Move the binary to your PATH
sudo mv kubectl /usr/local/bin/

# Verify the installation
kubectl version --client

# Configure kubectl to interact with your cluster (if necessary)
export KUBECONFIG=/path/to/your/kubeconfig
kubectl cluster-info

# Enable kubectl autocompletion (optional)
# For Bash
echo 'source <(kubectl completion bash)' >>~/.bashrc
source ~/.bashrc

# For Zsh
echo 'source <(kubectl completion zsh)' >>~/.zshrc
source ~/.zshrc
```

##### By following these comprehensive steps, you should have a secure and well-configured kubectl setup on your Linux system, ready to manage your Kubernetes clusters effectively.

### Step 1: Download the Latest Release of kubectl

#### Use curl to download the latest release of kubectl:

```sh
curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
```

### Step 2: Verify the Binary (Optional but Recommended)

#### To ensure the binary has not been tampered with, download the checksum file and verify the binary:

1. Download the checksum file:

```sh
curl -LO "https://dl.k8s.io/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl.sha256"
```

2. Verify the checksum:

```sh
echo "$(cat kubectl.sha256) kubectl" | sha256sum --check
```

##### If the output is kubectl: OK, the file is valid.

### Step 3: Make the kubectl Binary Executable

#### Change the permissions on the downloaded binary to make it executable:

```sh
chmod +x kubectl
```

### Step 4: Move the Binary to System PATH

#### Move the kubectl binary to a directory that is included in your system's PATH. Typically, this is /usr/local/bin:

```sh
sudo mv kubectl /usr/local/bin/
```

### Step 5: Verify the Installation

#### Verify that kubectl is installed correctly and that the version is up-to-date:

```sh
kubectl version --client
```

### Step 6: Configure kubectl to Interact with Your Cluster (if necessary)

#### Ensure that kubectl is configured to interact with your Kubernetes cluster. This typically involves setting up the kubeconfig file:

1. Set the KUBECONFIG environment variable (if you have a custom path for your kubeconfig file):

```sh
export KUBECONFIG=/path/to/your/kubeconfig
```

2. Verify connectivity to the cluster:

```sh
kubectl cluster-info
```

### Step 7: Enable kubectl Autocompletion (Optional)

#### To enable shell autocompletion for a better command-line experience:
##### For Bash:

```sh
echo 'source <(kubectl completion bash)' >>~/.bashrc
source ~/.bashrc
```

##### For Zsh:

```sh
echo 'source <(kubectl completion zsh)' >>~/.zshrc
source ~/.zshrc
```

## Install Minikube

### Summary of Minikube Commands

```sh
curl -LO https://storage.googleapis.com/minikube/releases/latest/minikube-linux-amd64 # Download using curl
# OR 
wget https://storage.googleapis.com/minikube/releases/latest/minikube-linux-amd64 # Download using wget
sudo install minikube-linux-amd64 /usr/local/bin/minikube # Install Minikube binary to /usr/local/bin
minikube version # Verify the installation
minikube start --driver=docker # Start Minikube with Docker driver
minikube status # Minikube running state
kubectl cluster-info # Interact with Minikube Cluster
minikube stop # Stop Minikube
minikube delete # Delete Minikube virtual machine
minikube dashboard # Minikube Dashboard: Access Kubernetes dashboard provided by Minikube
```

### Step 1: Download the Latest Release of kubectl

#### You can download and install Minikube using curl:

```sh
curl -LO https://storage.googleapis.com/minikube/releases/latest/minikube-linux-amd64
sudo install minikube-linux-amd64 /usr/local/bin/minikube
```

#### Alternatively, you can use wget:

```sh
wget https://storage.googleapis.com/minikube/releases/latest/minikube-linux-amd64
sudo install minikube-linux-amd64 /usr/local/bin/minikube
```

### Step 2: Verify the installation

```sh
minikube version
```

### Step 3: Start Minikube

```sh
minikube start --driver=docker
```

##### Minikube will download the necessary ISO and set up a local Kubernetes cluster. This might take a few minutes depending on your internet speed.

### Step 4: Verify Minikube running state

```sh
minikube status
```

### Step 5: Interact with Minikube Cluster

#### Once Minikube is running, you can interact with the Kubernetes cluster using kubectl. For example, to get cluster information:

```sh
kubectl cluster-info
```

### Step 6: Stop and Delete Minikube

#### When you're done working with Minikube, you can stop and delete the Minikube virtual machine:

```sh
minikube stop
minikube delete
```

### Additional Notes

 - **Driver Selection:** Minikube supports multiple drivers (docker, virtualbox, kvm2, etc.). The docker driver runs Kubernetes nodes as Docker containers, which is suitable for most development environments.
 - **Minikube Dashboard:** To access the Kubernetes dashboard provided by Minikube, run:
 ```sh
 minikube dashboard
 ```


## Deploy Application

### Step 1: Run Minikube

```sh
minikube start --driver=docker
```

### Step 2: Verify status

```sh
minikube status
```

### Step 3: Clone the Repository

```sh
git clone https://github.com/awsaf-utm/Deploy-.Net-Web-API-Service-using-Kubernetes-Minikube-on-Ubuntu.git 
```

### Step 4:  Go to the Dockerfile container folder

```sh
cd Deploy-.Net-Web-API-Service-using-Kubernetes-Minikube-on-Ubuntu/BookInformationService/BookInformationService
```

### Step 5: Build the docker image

```sh
docker build --build-arg ENVIRONMENT=Development -t engzaman2020/dotnet-web-api-v1-image:1 .
```

### Step 6: Load the Docker image into the Minikube 

```sh
minikube image load engzaman2020/dotnet-web-api-v1-image:1
```

### Step 7: Verify that the image is present in the Minikube Docker environment by running:

```sh
minikube ssh
docker images
exit
```

### Step 8: Set Up Kubernetes Deployment

```sh
kubectl apply -f dotnet-web-api-v1-deploy.yml
```

### Step 9: Verify deployments

#### To get same type of resources

```sh
kubectl get pods -o wide
kubectl get replicasets -o wide
kubectl get deployments -o wide
kubectl get service -o wide
kubectl get ingress 
```

#### To get all resources at once:

```sh
kubectl get all -o wide
```

#### To get specific resource details:

```sh
kubectl describe pod <pod-name>
kubectl describe replicaset <replicaset-name>
kubectl describe deployment <deployment-name>
kubectl describe service <service-name>
kubectl describe ingress <ingress-name>
```

### Step 10: Check Docker image environment variable is Development:

```sh
docker exec <docker-container> printenv # The Docker container must be in a running state.
docker inspect <docker-image>
```

#### Using Minikube:

 1. SSH into Minikube:
 ```sh
 minikube ssh
 ```
 2. Get the name of the pod running your container:
 ```sh
 kubectl get pods
 ```
 3. Describe the pod to see the environment variables:
 ```sh
 kubectl describe pod <pod-name>
 ```
 4. Or, you can exec into the pod and print the environment variables:
 ```sh
 kubectl exec -it <pod-name> -- printenv
 ```

#### Again, look for the ENVIRONMENT variable in the output.

### Step 11: Allow the port 3101 and 30101 to access:

```sh
sudo ufw allow 3101/tcp
sudo ufw allow 30101/tcp
```

### Step 12: Apply deployments

```sh
kubectl apply -f dotnet-web-api-v1-deploy.yml
```

### Step 13: Access the Application

 1. Retrieve the Minikube IP:
 
 ```sh
 minikube ip
 ```
 
 #### Assuming the Minikube IP is 192.168.49.2, access the application via:
 
 ```sh
 # http://<minikube-ip>:30101/
 curl http://192.168.49.2:30101/swagger/index.html
 curl http://192.168.49.2:30101/v1/BookInformation
 ```
 
 2. Alternatively, get the service URL directly:
 
 ```sh
 # minikube service <service-name> url
 minikube service dotnet-web-api-v1-service --url
 ```

### Step 14: Forward the service port

```sh
kubectl port-forward svc/dotnet-web-api-v1-service --address 0.0.0.0 8088:80
```

## Summary of Application deployment commands:

```sh
# Step 1: Run Minikube
minikube start --driver=docker # Start Minikube with Docker driver

# Step 2: Verify status
minikube status # Verify the status of Minikube

# Step 3: Clone the Repository
git clone https://github.com/awsaf-utm/Deploy-.Net-Web-API-Service-using-Kubernetes-Minikube-on-Ubuntu.git # Clone the repository

# Step 4: Go to the Dockerfile container folder
cd Deploy-.Net-Web-API-Service-using-Kubernetes-Minikube-on-Ubuntu/BookInformationService/BookInformationService # Navigate to the Dockerfile directory

# Step 5: Build the Docker image
docker build --build-arg ENVIRONMENT=Development -t engzaman2020/dotnet-web-api-v1-image:1 . # Build the Docker image with the Development environment

# Step 6: Load the Docker image into Minikube
minikube image load engzaman2020/dotnet-web-api-v1-image:1 # Load the Docker image into Minikube

# Step 7: Verify that the image is present in the Minikube Docker environment
minikube ssh # SSH into Minikube
docker images # List Docker images in Minikube
exit # Exit the Minikube SSH session

# Step 8: Set Up Kubernetes Deployment
kubectl apply -f dotnet-web-api-v1-deploy.yml # Apply the Kubernetes deployment

# Step 9: Verify deployments
# To get the same type of resources
kubectl get pods -o wide # List all pods with additional information
kubectl get replicasets -o wide # List all ReplicaSets with additional information
kubectl get deployments -o wide # List all deployments with additional information
kubectl get services -o wide # List all services with additional information
kubectl get ingress # List all ingress resources

# To get all resources at once:
kubectl get all -o wide # List all resources with additional information

# To get specific resource details:
kubectl describe pod <pod-name> # Describe a specific pod
kubectl describe replicaset <replicaset-name> # Describe a specific ReplicaSet
kubectl describe deployment <deployment-name> # Describe a specific deployment
kubectl describe service <service-name> # Describe a specific service
kubectl describe ingress <ingress-name> # Describe a specific ingress resource

# Step 10: Check Docker image environment variable is Development
docker exec <docker-container> printenv # The Docker container must be in a running state. Inspect environment variables
docker inspect <docker-image> # Inspect the Docker image

# Using Minikube:
minikube ssh # SSH into Minikube
kubectl get pods # Get the name of the pod running your container
kubectl describe pod <pod-name> # Describe the pod to see the environment variables
# Or, you can exec into the pod and print the environment variables:
kubectl exec -it <pod-name> -- printenv # Exec into the pod and print the environment variables
# Again, look for the ENVIRONMENT variable in the output.

# Step 11: Allow the ports 3101 and 30101 to access
sudo ufw allow 3101/tcp # Allow port 3101 through the firewall
sudo ufw allow 30101/tcp # Allow port 30101 through the firewall

# Step 12: Apply deployments
kubectl apply -f dotnet-web-api-v1-deploy.yml # Apply the Kubernetes deployment (again, if necessary)

# Step 13: Access the Application
# 1. Retrieve the Minikube IP:
minikube ip # Get the Minikube IP address

# Assuming the Minikube IP is 192.168.49.2, access the application via:
curl http://192.168.49.2:30101/swagger/index.html # Access the Swagger UI
curl http://192.168.49.2:30101/v1/BookInformation # Access the Book Information API

# 2. Alternatively, get the service URL directly:
minikube service dotnet-web-api-v1-service --url # Get the service URL

# Step 14: Forward the service port
kubectl port-forward svc/dotnet-web-api-v1-service --address 0.0.0.0 8088:80 # Forward the service port 
```


## Troubleshooting

### If you encounter application issues, follow these steps after updating source codes:

 1. Stop and remove all Docker containers if necessary
 ```sh
 docker stop $(docker ps -q) && docker rm $(docker ps -a -q)
 ```
 2. Go to the folder containing Dockerfile and Deployment files
 ```sh
 cd Deploy-.Net-Web-API-Service-using-Kubernetes-Minikube-on-Ubuntu/BookInformationService/ BookInformationService
 ```
 3. Delete the current deployment
 ```sh
 kubectl delete -f dotnet-web-api-v1-deploy.yml
 ```
 4. Remove the Docker image from both Docker and Minikube:
 ```sh
 docker rmi -f engzaman2020/dotnet-web-api-v1-image:1
 docker rmi $(docker images -q --filter "dangling=true")
 ```
 5. Update the repository:
 ```sh
 git pull
 ```
 6. Rebuild and load the Docker image
 ```sh
 docker build --build-arg ENVIRONMENT=Development -t engzaman2020/dotnet-web-api-v1-image:1 .
 minikube image load engzaman2020/dotnet-web-api-v1-image:1
 ```
 7. Reapply the Kubernetes deployment
 ```sh
 kubectl apply -f dotnet-web-api-v1-deploy.yml
 ```
 8. Test the application again
 ```sh
 curl http://192.168.49.2:30101/v1/BookInformation
 ```
 
