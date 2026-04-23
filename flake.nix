{
  description = "Nix devshells!";

  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
  };

  outputs = {nixpkgs, ...}: let
    system = "x86_64-linux";
    pkgs = import nixpkgs {inherit system;};
  in {
    devShells.${system}.default = pkgs.mkShell {
      packages = [pkgs.hello];
      nativeBuildInputs = with pkgs; [
        dotnetCorePackages.sdk_9_0
        dotnetCorePackages.runtime_9_0
      ];
    };
  };
}
