# Split string solution

def isEligibleWord(word,vowelsPerWord):
    count = 0
    for c in word:
        count+= 1 if c.lower() in ['a','e','i','o','u'] else 0
        if count>= vowelsPerWord:
            return True
    return False

def countWords(line, wordFrequency,vowelsPerWord):
    words = line.split(" ")
    count = 0
    for i in range(wordFrequency-1, len(words),wordFrequency):
        word = words[i]    
        count+= 1 if isEligibleWord(word,vowelsPerWord) else 0
    return count

def countInText(texts, wordFrequency, vowelsPerWord, lineFrequency):
    lineFrequency = max(lineFrequency,1)
    wordFrequency = max(wordFrequency,1)
    vowelsPerWord = max(vowelsPerWord,0)
    lines = texts.split("\n")
    lineCount = 0
    wordCount = 0
    for i in range(lineFrequency-1, len(lines),lineFrequency):
        line = lines[i]        
        noWordFound = countWords(line,wordFrequency,vowelsPerWord)
        lineCount+= 1 if noWordFound > 0 else 0
        wordCount+= noWordFound
    return lineCount,wordCount



f = open("sample.txt","r")
str =  f.read()

print(countInText(str,3,2,2))


f.close()




