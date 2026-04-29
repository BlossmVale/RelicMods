{
  description = "Nix devshells!";
  inputs = {
    nixpkgs.url = "github:nixos/nixpkgs/nixos-unstable";
  };
  outputs =
    { nixpkgs, ... }:
    let
      system = "x86_64-linux";
      pkgs = import nixpkgs { inherit system; };
    in
    {
      devShells.${system}.default = pkgs.mkShell {
        packages = [ pkgs.hello ];

        nativeBuildInputs = with pkgs; [
          dotnetCorePackages.sdk_9_0
        ];

        buildInputs = with pkgs; [
          # GUI / rendering
          gtk3
          glib
          cairo
          pango
          gdk-pixbuf
          libGL

          # Avalonia UI needs these
          fontconfig
          freetype
          xorg.libX11
          xorg.libXext
          xorg.libXrandr
          xorg.libXi
          xorg.libXcursor
          xorg.libXinerama
          xorg.libICE
          xorg.libSM
          xorg.libXt

          # Wayland (if needed)
          wayland
          libxkbcommon

          # Other common .NET native deps
          openssl
          zlib
          icu
          krb5

          desktop-file-utils

        ];

        shellHook = ''
          export LD_LIBRARY_PATH="${
            pkgs.lib.makeLibraryPath (
              with pkgs;
              [
                gtk3
                glib
                cairo
                pango
                gdk-pixbuf
                libGL
                fontconfig
                freetype
                xorg.libX11
                xorg.libXext
                xorg.libXrandr
                xorg.libXi
                xorg.libXcursor
                xorg.libXinerama
                xorg.libICE
                xorg.libSM
                xorg.libXt
                wayland
                libxkbcommon
                openssl
                zlib
                icu
                krb5
                desktop-file-utils
              ]
            )
          }:$LD_LIBRARY_PATH"

          export DOTNET_ROOT="${pkgs.dotnetCorePackages.sdk_9_0}"
        '';
      };
    };
}
