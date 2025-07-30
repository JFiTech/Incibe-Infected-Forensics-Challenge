# This is the solution for the decryption part of [Incibe's "Infected" Forensics Challenge](https://www.incibe.es/sites/default/files/paginas/academiahacker/retos-descargables/ficheros/infected.7z)

This Python script allows the decyption of the data that was being exfiltrated in the challenge.

### (This README will be updated later with more info)

## Usage:
Make sure you Python 3 installed on your system.

1. Create a Python venv (virtual environment) named `decryptor`:

    `
    python -m venv decryptor
    `

2. `cd` into the venv's directory:

    `
    cd decryptor
    `

3. Activate the Python virtual environment (venv):

    UNIX based systems (Linux/MacOS):

    `
    source bin/activate
    `

    Windows:

    `
    .\Scripts\activate
    `

4. Clone the repository:

    `
        git clone https://github.com/JFiTech/Incibe-Infected-Forensics-Challenge.git
    `

5. cd into the `Incibe-Infected-Forensics-Challenge` directory:

    `
    cd Incibe-Infected-Forensics-Challenge
    `

6. Install the dependencies:

    `
    pip install -r requirements.txt
    `

7. Running the program:

    `
    python DecryptorPoC.py 
    `

8. Deactivating the virtual environment (venv):

    `
    deactivate
    `