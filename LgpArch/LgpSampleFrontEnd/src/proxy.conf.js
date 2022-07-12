const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
    ],
    target: "https://localhost:49159",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
