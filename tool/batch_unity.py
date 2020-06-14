import subprocess
from pathlib import Path
from argparse import ArgumentParser
import platform
    

def main(args):
    project_path = Path(args.project_path).resolve()
    execute_method = args.execute_method
    log_path = Path(args.log_path).resolve()
    with open(project_path / 'ProjectSettings/ProjectVersion.txt') as f:
        version_config = {
            x.split(':')[0]: x.split(':')[1].lstrip(' ').rstrip('\n') for x in f.readlines()
        }
    unity_version = version_config['m_EditorVersion']
    system = platform.system()
    unity_path = None
    if system == 'Windows':
        unity_path = f'C:\\PROGRA~1\\Unity\\Hub\\Editor\\{unity_version}\\Editor\\Unity.exe'
    else:
        raise Exception(f'Not support such system [{system}]')
    print(f'start to batch unity {unity_version}')
    cmd = (
        f'{unity_path} -batchmode -quit'
        f' -executeMethod {execute_method}'
        f' -projectPath {str(project_path)}'
        f' -logFile {str(log_path)}'
    )
    p = subprocess.run(cmd)
    if p.returncode != 0:
        print('faild to batch unity')
        print(f'log file was created at [{log_path}]')
        exit(1)
                
    print('finish to batch unity')


if __name__ == '__main__':
    parser = ArgumentParser()
    parser.add_argument('--project-path', type=str, required=True)
    parser.add_argument('--execute-method', type=str, required=True)
    parser.add_argument('--log-path', type=str, required=True)
    
    args = parser.parse_args()
    main(args)