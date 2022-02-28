class ArgumentSyntaxBuilder {
    #build = "";
    #append(input) {
        if (!this.#build) {
            this.#build = input;
        } else {
            this.#build = `${this.#build} ${input}`;
        }
    }

    makeRegular(input, options) {
        if (typeof input !== "string") {
            throw new Error("SyntaxBuilder::makeRegular: Not a string");
        }

        const title = options?.title;
        if (title && typeof title !== "string") {
          throw new Error("SyntaxBuilder::makeRegular: Title not a string");
        }

        const inf = options?.inf;

        if (!options?.optional) {
            this.#append(`<${title ? `${title}: ` : ""}${input}${inf ? "..." : ""}>`);
        } else {
            this.#append(`<?${title ? `${title}: ` : ""}${input}${inf ? "..." : ""}>`);
        }

        return this;
    }

    makeChoice(inputs, options) {
        if (!Array.isArray(inputs)) {
            throw new Error("SyntaxBuilder::makeChoice: Not an array");
        }

        const def = options?.default;
        if (def && inputs.indexOf(def) === -1) {
            throw new Error("SyntaxBuilder::makeChoice: Default input not found");
        }

        if (options?.exactify) {
            inputs = inputs.map((el) => `"${el}"`);
        }

        const title = options?.title;
        if (title && typeof title !== "string") {
          throw new Error("SyntaxBuilder::makeRegular: Title not a string");
        }

        const inf = options?.inf;
        const splitString = inputs.join(" | ");

        if (!options?.optional) {
            this.#append(`<${title ? `${title}: ` : ""}${splitString}${def ? ` def = "${def}"` : ""}${inf ? "..." : ""}>`);
        } else {
            this.#append(`<?${title ? `${title}: ` : ""}${splitString}${def ? ` def = "${def}"` : ""}${inf ? "..." : ""}>`);
        }

        return this;
    }

    endBuild() {
        if (!this.#build) {
            throw new Error("SyntaxBuilder::endBuild: No build started");
        }

        const oldBuild = this.#build;
        this.#build = "";
        return oldBuild;
    }
}

const SyntaxBuilder = new ArgumentSyntaxBuilder();
const Command = ":eval";
const Syntax = SyntaxBuilder.makeRegular("JavaScript").endBuild();
console.log(Command + ` ${Syntax}`);