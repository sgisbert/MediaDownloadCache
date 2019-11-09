# MediaDownloadCache
Adds a cache layer to downloaded files from a url

## Usage

```c#
Download mdc = new Download();
MemoryStream stream = mdc.DownloadUrl(url, timeSpan, directory);
```

### Parameter explanation

**url** = file to download

**timeSpan** = Minutes to keep the file cached before downloading it again

**directory** = target folder to save the cached file.

## Flowchart diagram

![alt text](diagram.png "Flowchart diagram")

