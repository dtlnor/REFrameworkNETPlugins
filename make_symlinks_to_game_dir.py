# Script to deploy symlinks from our game folder to our build folder for various DLLs and other files

import fire
import os
import ctypes
import shutil

def symlink_main(gamedir=None, just_copy=False):
    if gamedir is None:
        print("Usage: make_symlinks_to_game_dir.py --gamedir=<path to game directory>")
        return
    
    # Throw an error if the user is not a privileged user
    try:
        is_admin = os.getuid() == 0
    except AttributeError:
        is_admin = ctypes.windll.shell32.IsUserAnAdmin() != 0

    if not is_admin and not just_copy:
        print("Error: This script must be run as an administrator")
        return
    
    game_plugin_dir = os.path.join(gamedir, "reframework", "plugins")
    # Get the current working directory
    if not os.path.exists(gamedir):
        print(f"Error: Directory {game_plugin_dir} does not exist")
        return

    src = os.path.abspath("plugins")
    dst = os.path.abspath(game_plugin_dir)
    if os.path.exists(dst):
        old_dst = os.path.join(os.path.dirname(dst), "old_" + os.path.basename(dst))
        os.rename(dst, old_dst)

    os.makedirs(os.path.dirname(dst), exist_ok=True)

    if just_copy:
        shutil.copytree(src, dst)
    else:
        os.symlink(src, dst)

    print("Symlinks created successfully")

if __name__ == '__main__':
    fire.Fire(symlink_main)