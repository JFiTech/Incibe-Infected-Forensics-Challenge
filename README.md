# This is the solution for the decryption part of [Incibe's "Infected" Forensics Challenge](https://www.incibe.es/sites/default/files/paginas/academiahacker/retos-descargables/ficheros/infected.7z)

This Python script allows the decyption of the data that was being exfiltrated in the challenge.

### (This README will be updated later with more info)

## Usage:
1. Create a Python venv (virtual environment) named `decryptor`:

    `
    python -m venv decryptor
    `

2. `cd` into the venv's directory.

3. Clone the repository:

    `
        git clone https://github.com/JFiTech/Incibe-Infected-Forensics-Challenge.git
    `

4. Install the dependencies:

    `
    pip install -r requirements.txt
    `


5. Activate the Python virtual environment (venv):

    UNIX based systems (Linux/MacOS):

    `
    source bin/activate
    `

    Windows:

    `
    .\Scripts\activate
    `


6. Running the program:

    `
    python DecryptorPoC.py 
    `

7. Deactivating the virtual environment (venv):

    `   
    deactivate
    `