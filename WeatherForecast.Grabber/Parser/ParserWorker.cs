﻿using System;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using WeatherForecast.Logger;

namespace WeatherForecast.Grabber.Parser
{
    class ParserWorker<T> where T : class
    {
        private readonly IParser<T> _parser;
        private readonly IParserSettings _parserSettings;
        private readonly HtmlLoader _loader;
        private ILog _logger;


        public event Action<object, T> OnNewData;
        public event Action<object> OnCompleted;

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings)
        {
            _logger = (new LoggerFactory()).GetLogger();
            _parser = parser;
            _parserSettings = parserSettings;
            _loader = new HtmlLoader(_parserSettings.BaseUrl);
        }

        #region Properties

        public int ResponseTime { get; set; } = 10000;
        public bool IsActive { get; private set; }

        #endregion

        public async void Start()
        {
            IsActive = true;
            await Work();
            IsActive = false;
            OnCompleted?.Invoke(this);
        }

        private async Task Work()
        {
            foreach (var parserSettingsPrefix in _parserSettings.Prefixes)
            {
                if (!IsActive)
                {
                    return;
                }

                var timerTask = Task.Delay(ResponseTime);
                var getSourceTask = _loader.GetSource(parserSettingsPrefix);

                await Task.WhenAny(timerTask, getSourceTask);

                if (getSourceTask.IsCompleted)
                {
                    await ParseHtml(getSourceTask.Result);
                }
                else
                {
                    _logger.Info($"Не удалось получить данные по адресу {_parserSettings.BaseUrl}{parserSettingsPrefix}");
                }
            }
        }

        private async Task ParseHtml(string htmlDock)
        {
            try
            {
                var domParser = new HtmlParser();
                var document = await domParser.ParseDocumentAsync(htmlDock);
                var result = _parser.Parse(document);
                OnNewData?.Invoke(this, result);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error deal with html parsing");
            }

        }
    }
}
