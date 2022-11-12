mergeInto(LibraryManager.library, {



  FetchData: function (str) {
    window.FetchDataOpensea();
    console.log("got str : "+ str);
  },



  GotData: function () {
    var returnStr = window.openseaData;
    var bufferSize = lengthBytesUTF8(returnStr) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(returnStr, buffer, bufferSize);
    return buffer;
  },



});