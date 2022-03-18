# Nicehash Exporter

## Setting up

Sample `docker-compose.yml`

```yaml
version: "3"
services:
  prometheus:
    image: prom/prometheus
    restart: unless-stopped
    volumes:
      - ./prometheus.yml:/etc/prometheus/prometheus.yml
      - ./data/prometheus.yml:/prometheus

  nicehash-poller:
    image: wiseowls/nicehash-exporter
    restart: unless-stopped
    environment:
      - NICEHASH_KEY=<API Key>
      - NICEHASH_SEC=<API Secret>
      - NICEHASH_ORG=<Organisation ID>
      #- NICEHASH_API=https://api2.nicehash.com # default value
      #- NICEHASH_prefix=nicehash # default value
      #- NICEHASH_coindeskPrefix=coindesk # default value
      #- NICEHASH_pollInterval=60000 # poll every minute by default
      #- NICEHASH_exporterHost=+ # default value
      #- NICEHASH_exporterPort=8088 # default value
  
    depends_on:
      - prometheus
```

Sample `prometheus.yml`

```yaml
global:
  scrape_interval: 15s
  evaluation_interval: 30s
  # scrape_timeout is set to the global default (10s).

scrape_configs:
  - job_name: 'nicehash'
    static_configs:
      - targets: ['nicehash-poller:8088']
```