# DevProxy Certificate Setup

## Overview
This directory should contain the PFX certificate that DevProxy uses to intercept HTTPS traffic.

## Required Certificate
- **Filename**: `rootCert.pfx`
- **Format**: PFX (Personal Information Exchange)
- **Password**: None (must not be password protected)
- **Trust**: Must be trusted on your machine

## How to Get the Certificate

### Option 1: Generate from DevProxy (Recommended)
1. Install DevProxy locally: `npm install -g @microsoft/dev-proxy`
2. Run DevProxy once: `devproxy --urls-to-watch "https://localhost:*/*"`
3. This will generate the certificate automatically
4. Copy `rootCert.pfx` from your DevProxy installation directory to this folder

### Option 2: Use Existing Certificate
If you already have DevProxy installed and configured:
1. Find your existing `rootCert.pfx` file (usually in `%USERPROFILE%/.devproxy/`)
2. Copy it to this directory

## Trust the Certificate
**Important**: You must trust this certificate on your machine or HTTPS requests will fail.

### Windows
1. Double-click `rootCert.pfx`
2. Install to "Local Machine" → "Trusted Root Certification Authorities"

### macOS/Linux
```bash
# Trust the certificate (adjust command based on your OS)
sudo security add-trusted-cert -d -r trustRoot -k /Library/Keychains/System.keychain rootCert.pfx
```

## Verification
Once the certificate is in place and trusted:
- File exists: `.devproxy/cert/rootCert.pfx`
- Certificate is trusted on your machine
- DevProxy can intercept HTTPS traffic for testing

## Note
The certificate is git-ignored for security reasons. Each developer needs to set up their own certificate.
