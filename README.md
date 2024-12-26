# Distributed Online Trading System

## Overview
This project is a distributed online trading system designed for monitoring data, executing strategies, and operating seamlessly across multiple servers. It consists of **three main modules**: **Queue Module**, **Producer Module**, and **Consumer Module**. The architecture ensures scalability, modularity, and performance monitoring.

---

## Project Modules
### 1. **Producer Module**
- **Purpose**: Fetches data from APIs or CSV files during backtesting.
- **Key Features**:
  - Fetches data from multiple sources including **OANDA**, **TwelveData**, and **CSV files**.
  - Provides backtesting capabilities using historical data.
  - Allows integration of new data providers with minimal code changes by implementing the **IOrderOperations** interface.
  - Saves fetched data into the queue module for further processing.

### 2. **Queue Module**
- **Purpose**: Stores and manages data fetched by the producer module in a queue.
- **Key Features**:
  - Maintains performance logs of both **Producer** and **Consumer** modules.
  - Acts as a decoupling layer between modules, ensuring minimal dependency.
  - Supports distributed execution by enabling monitoring and scaling.

### 3. **Consumer Module**
- **Purpose**: Executes trading strategies based on data fetched in the queue.
- **Key Features**:
  - Processes data based on **instrument type**, enabling different strategies for different markets.
  - Provides multi-container deployment, where each version of the module can run independently.
  - Supports three main data sources: **OANDA**, **TwelveData**, and **CSV files**.
  - Offers backtesting capabilities for strategy validation.
  - Enables live trading and order execution via the **OANDA API**.
  - Simplifies adding new strategies by extending the **TraderAgent** class with additional methods.

---

## Key Design Principles
- **Scalability**: Each module can operate independently and scale horizontally by running on separate containers.
- **Minimal Dependencies**: Modules communicate exclusively through the queue system, reducing inter-module dependencies.
- **Extensibility**: New data providers and strategies can be added with minimal code modifications.
- **Separation of Concerns**: Each module handles a specific responsibility, ensuring better maintainability and flexibility.

---

## Implementation Details
- **Programming Language**: C#
- **Database**: SQL Server
- **Architecture**: Distributed and containerized for scalability and modular deployment.
- **Communication**: Message queue-based communication between modules.

---

## Goals
- Provide a modular and scalable trading platform.
- Simplify integration of new data sources and strategies.
- Enable live trading, backtesting, and performance monitoring.
- Ensure seamless deployment and monitoring across multiple servers.

---

## Setup Instructions
1. Clone the repository.
2. Configure the database and connection strings.
3. Install dependencies and build the project.
4. Deploy each module as a separate container or run as a single project.
5. Configure API keys for data providers in the configuration file.

---

## Contribution
Contributions are welcome! Feel free to submit issues or pull requests to improve functionality.

---

## License
This project is licensed under the **MIT License**. See the LICENSE file for more details.

