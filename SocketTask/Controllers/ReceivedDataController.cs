using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocketTask.Interfaces;
using SocketTask.Models;

namespace SocketTask.Controllers
{
    /// <summary>
    /// Received data controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ReceivedDataController : Controller
    {
        /// <summary>
        /// The receive data repository
        /// </summary>
        private readonly IReceiveDataRepository _receiveDataRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceivedDataController" /> class.
        /// </summary>
        /// <param name="receiveDataRepository">The receive data repository.</param>
        /// <param name="socketCommunication">The socket communication.</param>
        public ReceivedDataController(IReceiveDataRepository receiveDataRepository, ISocketCommunication socketCommunication)
        {
            _receiveDataRepository = receiveDataRepository;
            socketCommunication.DataReceived += (s, e) => { Index().RunSynchronously(); };
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listOfDatas = await _receiveDataRepository.GetAll();

            return View("ReceiveDataView", listOfDatas);
        }
    }
}