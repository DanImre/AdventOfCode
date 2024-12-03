from sympy import Symbol, nonlinsolve
import time

# There are 3 equations per rock
# I have 6 unknown variable
# With each rock I bring 1 more T_i
# That means each rock has the ability to solve 2 additional variables
# thus 3 rock is enough

# test with 3 rocks :
#       344525619959965 437880958119624 242720827369528
#       SUM: 1025127405449117

# test with 4 rocks :
#       344525619959965 437880958119624 242720827369528
#       SUM: 1025127405449117

# I was right: '1025127405449117' is the solution

# Runtime 0.42 secs

PMx = Symbol('PMx')
PMy = Symbol('PMy')
PMz = Symbol('PMz')

VMx = Symbol('VMx')
VMy = Symbol('VMy')
VMz = Symbol('VMz')

symbols = [PMx, PMy, PMz, VMx, VMy, VMz]

equations = []
iteration = 0

with open('AOC23-24INPUT.txt', 'r') as file:
    for line in file:
        pos, vel = line.strip().split('@')
        row = []
        for i in pos.split(',') + vel.split(','):
            row.append(int(i))

        Px, Py, Pz, Vx, Vy, Vz = row
        
        t = Symbol(f"t{iteration}")
        symbols.append(t)

        equations.append(PMx + VMx*t - Px - Vx*t)
        equations.append(PMy + VMy*t - Py - Vy*t)
        equations.append(PMz + VMz*t - Pz - Vz*t)
        iteration += 1

        if(iteration >= 3):
            break

solution = list(nonlinsolve(equations, symbols))

print(solution[0], solution[1], solution[2])
print(f"SUM: {sum(solution[:3])}")