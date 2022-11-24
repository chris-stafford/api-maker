using System.Text;

var outputDir = "/Users/CStafford/gac/imov2-api";
var sourceDir = "/Users/CStafford/gac/api-maker/skeleton";

var entities = new []
{
    "User",
    "Item",
    "Box",
    "Location",
    "Preference",
    "Project",
    "Reminder",
    "Share",
    "Image",
    "Notification",
    "Category"
};

void run(string srcDir, string destDir, string entity = null)
{
    if (!Directory.Exists(destDir))
    {
        Directory.CreateDirectory(destDir);
    }

    foreach (var srcFile in Directory.GetFiles(srcDir))
    {
        var fileName = srcFile.Split('/').Last();
        if (!fileName.Contains("xxx", StringComparison.InvariantCultureIgnoreCase))
        {
            write(srcFile, $"{destDir}/{fileName}", entity);
        }
        else if (entity != null)
        {
            var modifiedDestFileName = subInEntity(fileName, entity);
            write(srcFile, $"{destDir}/{modifiedDestFileName}", entity);
        }
        else
        {
            foreach (var entityToSub in entities)
            {
                var modifiedDestFileName = subInEntity(fileName, entityToSub);
                write(srcFile, $"{destDir}/{modifiedDestFileName}", entityToSub);
            }
        }
    }

    foreach (var subDir in Directory.GetDirectories(srcDir))
    {
        var dirName = subDir.TrimEnd('/').Split('/').Last();

        if (new[] { "bin", "obj" }.Any(dirName.Equals))
        {
            continue;
        }
        
        if (!dirName.Contains("xxx", StringComparison.InvariantCultureIgnoreCase))
        {
            run(subDir, $"{destDir}/{dirName}", entity);
        }
        else
        {
            foreach (var entityToSub in entities)
            {
                var modifiedDestFileName = subInEntity(dirName, entityToSub);
                run(subDir, $"{destDir}/{modifiedDestFileName}", entityToSub);
            }
        }
    }
}

void write(string srcFile, string destFile, string entity = null)
{
    var text = File.ReadAllText(srcFile);
    
    if (entity != null)
    {
        text = subInEntity(text, entity);
        File.WriteAllText(destFile, text);
        return;
    }
    var toWrite = new StringBuilder();

    foreach (var line in text.Replace("\r", string.Empty).Split('\n'))
    {
        if (line.Contains("xxx", StringComparison.InvariantCultureIgnoreCase))
        {
            foreach (var entityToSub in entities)
            {
                var subbedLine = subInEntity(line, entityToSub);
                toWrite.AppendLine(subbedLine);
            }
        }
        else
        {
            toWrite.AppendLine(line);
        }
    }

    File.WriteAllText(destFile, toWrite.ToString());
}

string subInEntity(string text, string entity)
{
    var entityPlural = entity + "s";
    if (entity.EndsWith("x"))
    {
        entityPlural = entity + "es";
    }
    if (entity.EndsWith("y"))
    {
        entityPlural = entity.Substring(0, entity.Length - 1) + "ies";
    }

    text = text
        .Replace("Xxxs", entityPlural)
        .Replace("xxxs", entityPlural.ToLower())
        .Replace("Xxx", entity)
        .Replace("xxx", entity.ToLower());
    
    return text;
}

run(sourceDir, outputDir);
