# Upgrade solution2 to handle data streaming/callback/async

class CountWords:
    def __init__(self,wordFrequency, vowelsPerWord, lineFrequency):
        self.wordFrequency =  max(wordFrequency,1)
        self.vowelsPerWord =  max(vowelsPerWord,0)
        self.lineFrequency =  max(lineFrequency,1)

        self.linei = 1
        self.wordi = 0  
        self.wordCount = 0
        self.lineCount = 0
        self.word = []
        self.state = 0 # StartLine
        self.lastLineHit = False

    @staticmethod
    def encodeChar(c):
        if c== ' ':
            return 0
        if c == '\n':
            return 1
        if c.lower() in ['a','e','i','o','u']:
            return 3
        return 2

    @staticmethod
    def isEligibleWord(word,vowelsPerWord):
        count = 0
        for c in word:
            count+= 1 if c.lower() in ['a','e','i','o','u'] else 0
            if count>= vowelsPerWord:
                return True
        return False


    def processChar(self,c):
        charType = CountWords.encodeChar(c)
        isAlpha = charType > 1
        
        matchedLine = self.linei % self.lineFrequency == 0

        if self.state == 0: #Start State
            if isAlpha == True:
                self.state = 1
                self.word.append(c)
        else: 
            if self.state == 1: #Word construction state        
                if isAlpha == False: #Found a word
                    self.wordi += 1 if matchedLine == True else -self.wordi
                        
                    matched = matchedLine & ((self.wordi) % self.wordFrequency == 0)
                    d = d = "".join(self.word)
                    inc = 1 if (matched & CountWords.isEligibleWord(d,self.vowelsPerWord)) == True else 0

                    self.wordCount+= inc
                    self.lastLineHit = self.lastLineHit or inc > 0                                        
                    self.word = [] # Reset word
                    self.state = 0 # come back start state
                else:
                    self.word.append(c)
                    
        if charType == 1:
            self.linei+=1
            self.lineCount+=1 if self.lastLineHit == True else 0
            self.lastLineHit = False
    
    def getCount(self, finalized=False):
        lineCount = self.lineCount
        wordCount = self.wordCount
        lastLineHit = self.lastLineHit
        if finalized == True:         
            lineCount = self.lineCount
            
            matchedLine = self.linei % self.lineFrequency == 0
            
            if len(self.word) > 0:
                matched = matchedLine & ((self.wordi+1) % self.wordFrequency == 0)
                d = d = "".join(self.word)
                inc = 1 if (matched & CountWords.isEligibleWord(d,self.vowelsPerWord)) == True else 0
                lastLineHit = lastLineHit or inc > 0
                wordCount+= inc
        lineCount+=1 if lastLineHit == True else 0
           
        return lineCount,wordCount


myCounter = CountWords(3,2,2)

f = open("sample.txt","r")
str =  f.read()
i = 0
for c in str:
    #if i == 200: break
    myCounter.processChar(c)
    i+=1

print(myCounter.getCount(True))

f.close()  



    
