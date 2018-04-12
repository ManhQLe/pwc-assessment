def Count(texts):
    state = 0    
    count = 0
    for c in texts:
        isAlpha = c!=' ' and c!='\n'
        if state == 0:
            state = 1 if isAlpha==True else 0
            count+=1 if isAlpha==True else 0
        else: #word state
            if isAlpha == False:                
                state = 0

    return count

i = 1

x = True

x = x & i>=1

print(x)


f = open("sample2.txt","r")
str =  f.read()

print(Count(str))


f.close()


