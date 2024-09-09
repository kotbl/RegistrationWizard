const { env } = require("process");

const target = env.ASPNETCORE_HTTPS_PORT
  ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}`
  : env.ASPNETCORE_URLS
  ? env.ASPNETCORE_URLS.split(";")[0]
  : "http://localhost:5256";

const PROXY_CONFIG = [
  {
    context: ["/Country"],
    target,
    secure: false,
  },
  {
    context: ["/Province"],
    target,
    secure: false,
  },
  {
    context: ["/User"],
    target,
    secure: false,
  },
];

module.exports = PROXY_CONFIG;
