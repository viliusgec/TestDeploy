# TestDeploy

Create new image and deploy it:
  - docker tag <imageID> registry.digitalocean.com/vilgec-personal-registry/testdeployment_api:{version}
  - docker push registry.digitalocean.com/vilgec-personal-registry/testdeployment_api:{version}
  - kubectl apply -f manifest.yaml
