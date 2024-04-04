/* Copyright 2014 Mozilla Foundation
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

'use strict';

if (!pdfjsLib.getDocument || !pdfjsViewer.PDFViewer)  {
  alert('Please build the pdfjs-dist library using\n' +
        '  `gulp dist-install`');
}

// The workerSrc property shall be specified.
//
pdfjsLib.GlobalWorkerOptions.workerSrc =
  '../../js/pdf.worker.js';

// Some PDFs need external cmaps.
//
var CMAP_URL = '../../cmaps/';
var CMAP_PACKED = true;

var DEFAULT_URL = '../../../pdfs/compressed.tracemonkey-pldi-09.pdf';
var SEARCH_FOR = ''; // try 'Mozilla';


var pdfData = "";

$.ajax({

    type: "get", //如果报405错误就把post改成get
    async: false,
    mimeType: 'text/plain; charset=x-user-defined',
    url: "/Handler/ReadPdfHandler.ashx", //请求服务器数据 
    success: function (data) {

        pdfData = data; //data就是byte[]数组，下面有介绍  

    }
    , complete: function (xhr, errorText, errorType) {

        //debugger;
        //var p = "";
        //alert("请求完成后");
        //console.log(xhr.getAllResponseHeaders());

        var contentDisposition = xhr.getResponseHeader('Content-Disposition');
        var arr = contentDisposition.split(";filename=");
        var filename = decodeURI(arr.length > 0 ? arr[1] : ""); // 解析URL编码
        //filename = decodeURI(filename);


        //console.log(filename);

    }
    , error: function (xhr, errorText, errorType) {
        //alert("请求错误后");
    }
    , beforSend: function () {
        //alert("请求之前");
    }

});









var container = document.getElementById('viewerContainer');

// (Optionally) enable hyperlinks within PDF files.
var pdfLinkService = new pdfjsViewer.PDFLinkService();

var pdfViewer = new pdfjsViewer.PDFViewer({
  container: container,
  linkService: pdfLinkService,
});
pdfLinkService.setViewer(pdfViewer);

// (Optionally) enable find controller.
var pdfFindController = new pdfjsViewer.PDFFindController({
  pdfViewer: pdfViewer,
});
pdfViewer.setFindController(pdfFindController);

container.addEventListener('pagesinit', function () {
  // We can use pdfViewer now, e.g. let's change default scale.
  pdfViewer.currentScaleValue = 'page-width';

  if (SEARCH_FOR) { // We can try search for things
    pdfFindController.executeCommand('find', {query: SEARCH_FOR});
  }
});

// Loading document.
//pdfjsLib.getDocument({
//  url: DEFAULT_URL,
//  cMapUrl: CMAP_URL,
//  cMapPacked: CMAP_PACKED,
//}).then(function(pdfDocument) {
//  // Document loaded, specifying document for the viewer and
//  // the (optional) linkService.
//  pdfViewer.setDocument(pdfDocument);
//  pdfLinkService.setDocument(pdfDocument, null);
//});

pdfjsLib.getDocument({
    data: pdfData,
    cMapUrl: CMAP_URL,
    cMapPacked: CMAP_PACKED,
}).then(function (pdfDocument) {
    // Document loaded, specifying document for the viewer and
    // the (optional) linkService.
    pdfViewer.setDocument(pdfDocument);

    pdfLinkService.setDocument(pdfDocument, null);
});
