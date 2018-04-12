#   ' ' = 0, '\n' = 1 , non-vowels = 2, vowels=3
# FSM based solution

def encodeChar(c):
    if c== ' ':
        return 0
    if c == '\n':
        return 1
    if c.lower() in ['a','e','i','o','u']:
        return 3
    return 2
   
def isEligibleWord(word,vowelsPerWord):
    count = 0
    for c in word:
        count+= 1 if c.lower() in ['a','e','i','o','u'] else 0
        if count>= vowelsPerWord:
            return True
    return False

def countInText(texts, wordFrequency, vowelsPerWord, lineFrequency):    
    lineFrequency = max(lineFrequency,1)
    wordFrequency = max(wordFrequency,1)
    vowelsPerWord = max(vowelsPerWord,0)  
    linei = 1
    wordi = 0  
    wordCount = 0
    lineCount = 0
    word = []
    state = 0 # StartLine
    lastLineHit = False

    for c in texts:
        charType = encodeChar(c)
        isAlpha = charType > 1
        
        matchedLine = linei % lineFrequency == 0

        if state == 0: #Start State
            if isAlpha == True:
                state = 1
                word.append(c)
        else: 
            if state == 1: #Word construction state        
                if isAlpha == False: #Found a word
                    wordi += 1 if matchedLine == True else -wordi
                        
                    matched = matchedLine & ((wordi) % wordFrequency == 0)
                    d = d = "".join(word)
                    inc = 1 if (matched & isEligibleWord(d,vowelsPerWord)) == True else 0

                    wordCount+= inc
                    lastLineHit = lastLineHit or inc > 0                                        
                    word = [] # Reset word
                    state = 0 # come back start state
                else:
                    word.append(c)
                    
        if charType == 1:
            linei+=1
            lineCount+=1 if lastLineHit == True else 0
            lastLineHit = False
        

    # Process last word and last line
    if len(word) > 0:
        matched = matchedLine & ((wordi+1) % wordFrequency == 0)
        d = d = "".join(word)
        inc = 1 if (matched & isEligibleWord(d,vowelsPerWord)) == True else 0
        lastLineHit = lastLineHit or inc > 0
        wordCount+= inc
    lineCount+=1 if lastLineHit == True else 0

    return lineCount, wordCount


f = open("sample.txt","r")
str =  f.read()

print(countInText(str,3,2,2))


f.close()