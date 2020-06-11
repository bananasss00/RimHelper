import fnmatch, os, shutil

DEVENV_PATH = f'c:\\Programs\\VS2019\\Common7\\IDE\\devenv.exe'

def clean_project(dir):
    [shutil.rmtree(f'{dir}/{x}/') for x in ['obj', 'bin'] if os.path.isdir(f'{dir}/{x}')]

def copy_and_overwrite(from_path, to_path):
    if os.path.exists(to_path):
        shutil.rmtree(to_path)
    shutil.copytree(from_path, to_path)

def fix_harmony_namespace(project):
    for root, dirnames, filenames in os.walk(project):
        for filename in fnmatch.filter(filenames, '*.cs'):
            cs = os.path.join(root, filename)
            with open(cs, 'r+', encoding='utf-8') as sln:
                text: str = sln.read()
                if 'using Harmony;' in text:
                    sln.seek(0)
                    text = text.replace('using Harmony;', 'using HarmonyLib;')
                    sln.write(text)
def fix_csproj(fn,replacements = None):
    with open(fn, 'r+', encoding='utf-8') as sln:
        text: str = sln.read()
        sln.seek(0)
        text = text.replace('..\\packages\\RW10\\', '..\\packages\\RW11\\')\
            .replace('UnityEngine', 'UnityEngine.CoreModule')\
            .replace('<TargetFrameworkVersion>v3.5</TargetFrameworkVersion>', '<TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>')
        if replacements is not None:
            for r in replacements:
                text = text.replace(r['from'], r['to'])
        sln.write(text)

if __name__ == '__main__':
    inp = input('\ntake action:\n1 - create RW 1.1 solution\n2 - create RW 1.1 solution and compile\n')

    [clean_project(dir) for dir in ['DebugLibrary', 'RimHelperProxyMod', 'RimHelper', 'IPCInterface']]
    copy_and_overwrite('RimHelperProxyMod', 'RimHelperProxyMod-1.1')
    copy_and_overwrite('DebugLibrary', 'DebugLibrary-1.1')
    shutil.copyfile('RimHelper.sln', 'RimHelper-1.1.sln')
    # fix references
    fix_harmony_namespace('RimHelperProxyMod-1.1')
    fix_harmony_namespace('DebugLibrary-1.1')
    fix_csproj('RimHelperProxyMod-1.1/RimHelperProxyMod.csproj', [
        {'from': '\\RimHelperProxyMod\\Assemblies\\', 'to': '\\RimHelperProxyMod\\v1.1\\Assemblies\\'}
    ])
    fix_csproj('DebugLibrary-1.1/DebugLibrary.csproj')
    with open('RimHelperProxyMod-1.1/Harmony/HM.cs', 'r+', encoding='utf-8') as sln:
        text: str = sln.read()
        sln.seek(0)
        text = text.replace('HarmonyInstance.Create', 'new HarmonyLib.Harmony')\
            .replace('HarmonyInstance', 'HarmonyLib.Harmony')
        sln.write(text)
    # fix solution
    with open('RimHelper-1.1.sln', 'r+', encoding='utf-8') as sln:
        text: str = sln.read()
        sln.seek(0)
        text = text.replace('"RimHelperProxyMod\\RimHelperProxyMod.csproj"', '"RimHelperProxyMod-1.1\\RimHelperProxyMod.csproj"')\
            .replace('"DebugLibrary\\DebugLibrary.csproj"', '"DebugLibrary-1.1\\DebugLibrary.csproj"')
        sln.write(text)

    if inp == '2':
        if os.path.isfile(DEVENV_PATH):
            os.system(f'"{DEVENV_PATH}" /build Release RimHelper.sln')
            os.system(f'"{DEVENV_PATH}" /build Release RimHelper-1.1.sln')
            [clean_project(dir) for dir in ['DebugLibrary', 'RimHelperProxyMod', 'RimHelper', 'IPCInterface', 'DebugLibrary-1.1', 'RimHelperProxyMod-1.1']]
            os.rename('_Release_/RimHelperProxyMod/Common/Assemblies/SharedMemory.dll', '_Release_/RimHelperProxyMod/Common/Assemblies/$haredMemory.dll')
            shutil.copyfile('_Release_/RimHelperProxyMod/Common/Assemblies/$haredMemory.dll', '_Release_/RimHelperProxyMod/Assemblies/$haredMemory.dll')
            shutil.copyfile('_Release_/RimHelperProxyMod/Common/Assemblies/0IPCInterface.dll', '_Release_/RimHelperProxyMod/Assemblies/0IPCInterface.dll')
        else:
            print(f'BAD DEVENV PATH: {DEVENV_PATH}')

    input('Press any key')

