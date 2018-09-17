import os
import sys
import xml.etree.ElementTree as ET
from git import Repo



def main():
    if len(sys.argv) < 2:
        print("please set version!")
    else:
        proj_file = './QComm/QComm.csproj'

        # set version on proj
        tree = ET.parse(proj_file)
        root = tree.getroot()
        version = root.find("./PropertyGroup/Version")
        version.text = sys.argv[1]
        tree.write(proj_file)
        print(version.tag, version.text)

        # add tag and push
        repo = Repo(os.path.abspath("./"))
        repo.git.commit('-am', 'v update')
        new_tag = repo.create_tag("v"+sys.argv[1])
        repo.remotes.origin.push("--follow-tags")
        print("pushed", new_tag)

if __name__ == "__main__":
    main()


