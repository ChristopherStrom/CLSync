# CLSync

CLSync is a Windows application that synchronizes a local directory with a remote SFTP server. The application runs in the system tray and periodically checks for file changes to upload new or updated files and delete files that no longer exist locally.

## Features

- Sync local directory with remote SFTP server
- Periodic sync with configurable duration
- Handles nested directories
- Compares file hashes to minimize unnecessary uploads
- Deletes remote files that no longer exist locally
- Runs in the system tray
- Configurable via a settings form

## Configuration

The application saves its configuration and log files in the `c:\\CLSync\\` directory. 

### Configuration Settings

- **Host**: The SFTP server hostname
- **Port**: The SFTP server port
- **Username**: The SFTP username
- **Password**: The SFTP password
- **Local Directory**: The local directory to sync
- **Remote Directory**: The remote directory to sync
- **Sync Duration**: The duration between syncs in seconds

## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/ChristopherStrom/CLSync.git
    ```

2. Open the solution in Visual Studio.

3. Build the project to generate the executable.

## Usage

1. Run the application. It will minimize to the system tray.

2. Right-click the system tray icon and select "Config" to open the configuration form.

3. Fill in the required fields and save the configuration.

4. The application will start syncing according to the configured sync duration.

## Logging

The application logs its operations to `c:\\CLSync\\sync.log`.

## Contributing

1. Fork the repository.
2. Create a new branch.
3. Make your changes.
4. Submit a pull request.

## License

This project is licensed under the MIT License.
