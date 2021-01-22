using CCWFortune.Services.Interfaces;
using Jil;
using Microsoft.Extensions.Configuration;
using NLog.Fluent;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCWFortune.Services
{
    public class RedisService
    {
        private readonly string _redisHost;        
        private readonly int _redisPort;
        private readonly string _redisPass;
        private ConnectionMultiplexer _redis;
        public RedisService(IConfiguration config)
        {
            _redisHost = config["Redis:Host"];
            _redisPort = Convert.ToInt32(config["Redis:Port"]);
            _redisPass = config["Redis:Pass"];
        }
        public void Connect()
        {
            try
            {
                var configString = $"{_redisHost}:{_redisPort},password={_redisPass},connectRetry=5";
                _redis = ConnectionMultiplexer.Connect(configString);
            }
            catch (RedisConnectionException err)
            {
                Log.Error(err.ToString());
                throw err;
            }
            Log.Debug("Connected to Redis");
        }
        public bool Set(string key, string message)
        {
            var db = _redis.GetDatabase();            
            return db.StringSet(key, message);
        }
        public string Get(string key)
        {
            var db = _redis.GetDatabase();
            return db.StringGet(key);
        }
    }
}
