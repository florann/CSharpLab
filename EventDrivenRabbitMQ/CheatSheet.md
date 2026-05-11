# containerd
## Overview

Runtime used initialy by Docker to run containers. Now it's the most popular container runtime.

# Nerdctl
## Overview

CLI tools used for a direct communication to containerd runtime. It has been created to replace docker CLI because Docker Daemon has been deprecated. ( This daemon must run with elevated priviledge which causes security issue )

### Commands

There is a transparency to all docker commands. 

- `nerdctl build .` : Build an image 
> `-t`: specify the image tag

- `nerdctl save [image_tag]` : Save an image to tar archive ( it seems to be save in the nerdctl CLI registery )

- `nerdctl --namespace [name_space] load` : Load an image to a specific namespace

> Chaining both commands allow to load an image built into a kubernetes cluster


# Kubectl
## Overview

Kubernetes CLI tools to manage a Kubernetes cluster. 

### Informations 

- Node : One machine ( physical or virtual ) inside the cluster
- Pod : One or more containers running inside a node
- Image : Blueprint used to create a container inside a pod

### Commands

- `kubectl get nodes` : List all nodes 
- `kubectl get pod` : List all pods inside the cluster 
- `kubectl get image` : List all images register in the cluster
- `kubectl apply -f [file_name]` : Apply the specific configuration for a resource
- `kubectl port-forward [service_name] [port_forward] -n [namespace]` : Create a port forwarding from the host machine to the Kubernetes cluster
- `kubectl logs -n [namespace]` : Get logs for the specific namespace 
> The log command can be used on every Kubernetes resources

### Libraries

**KEDA** : Kubernetes event driven autoscaling

An operator running inside a Kubernetes cluster that allow automatic scaling / downscaling event based.

`helm install keda kedacore/keda --namespace keda --create-namespace` : Install command for Keda

> `helm` is Kubernetes package manager. List of command to install KEDA in a cluster 
>> helm repo add kedacore https://kedacore.github.io/charts
helm repo update
helm install keda kedacore/keda --namespace keda --create-namespace

`kubectl get scaledobject -n [namespace]` : Get informations related to objects scaler for the given namespace

`kubectl describe scaledobject [scale_object_name] -n [namespace]` : Get a JSON description of the given scale object name 

---